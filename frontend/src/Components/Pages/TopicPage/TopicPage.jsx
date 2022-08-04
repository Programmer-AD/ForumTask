import React from "react";
import Api from "../../../Api/ApiUnited.js";
import css from "./style.module.css"
import MessageList from "./MessageList/MessageList.jsx";
import NotFoundPage from "../NotFoundPage/NotFoundPage.jsx";
import TopicView from "./TopicView/TopicView.jsx";
import PageChooser from "../../Common/PageChooser/PageChooser.jsx";
import MessageInput from "./MessageInput/MessageInput.jsx";

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);

        let page = this.props.match.params.page ?? 1, topicId = this.props.match.params.topicId;
        if (page == 0) {
            this.props.history.push("/not-found");
        }
        page--;

        this.state = {
            topic: null,
            list: [],
            pageCount: 0,
            topicId,
            page,
            canModal: true,
            notFound: false
        };

        this.handleMessageChange = this.handleMessageChange.bind(this);
        this.handleTopicChange = this.handleTopicChange.bind(this);
        this.handleModal = this.handleModal.bind(this);
    }
    async loadAll(retryCount = 3) {
        try {
            const topic = await Api.topic.get(this.state.topicId);

            this.setState({ topic: topic });

            this.reload();
        } catch (e) {
            if (e.code === 404) {
                this.setState({ notFound: true });
            } else if (retryCount > 0) {
                this.loadAll(retryCount - 1);
            }
        }
    }
    reload() {
        Api.message.getPageCount(this.state.topicId).then(pageCount => this.setState({ pageCount: pageCount }));
        Api.message.getTopOld(this.state.topicId, this.state.page).then(list => this.setState({ list }));
    }

    componentDidMount() {
        this.loadAll();
    }

    handleMessageChange() {
        this.loadAll();
    }

    handleTopicChange() {
        this.loadAll();
    }

    handleModal(free) {
        this.setState({ canModal: free });
    }

    render() {
        if (this.state.notFound)
            return <NotFoundPage />
        if (this.state.topic === null)
            return <div>Loading...</div>
        return (<>
            <TopicView value={this.state.topic} user={this.props.user} onChange={this.handleTopicChange} onModal={this.handleModal} canModal={this.state.canModal} />
            <MessageList list={this.state.list} user={this.props.user} onChange={this.handleMessageChange} onModal={this.handleModal} canModal={this.state.canModal} />
            <MessageInput topicId={this.state.topicId} onSend={this.handleMessageChange} user={this.props.user} />
            <PageChooser current={this.state.page + 1} count={this.state.pageCount} getUrl={(v) => `/topic-${this.state.topicId}/page-${v}`} />
        </>);
    }
} 
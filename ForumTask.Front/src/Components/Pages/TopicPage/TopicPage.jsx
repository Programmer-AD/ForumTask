import React from "react";
import Api from "../../../Api/ApiUnited.js";
import css from "./style.module.css"
import MessageList from "./MessageList/MessageList.jsx";
import NotFoundPage from "../NotFoundPage/NotFoundPage.jsx";
import TopicView from "./TopicView/TopicView.jsx";
import PageChooser from "../../Common/PageChooser/PageChooser.jsx";
import MessageInput from "./MessageInput/MessageInput.jsx";

export default class TopicPage extends React.Component{
    constructor(props){
        super(props);

        let page=this.props.match.params.page??1,topicId=this.props.match.params.topicId;
        if (page==0)
            this.props.history.push("/not-found");
        page--;
        this.state={
            topic:null,
            list:[],
            pageCount:0,
            topicId,
            page,
            showModal:false,
            notFound:false
        };

        this.handleChange=this.handleChange.bind(this);
    }
    loadAll(tc){
        Api.topic.get(this.state.topicId).then(
            (t)=>{
                this.setState({topic:t});
                this.reload();
            }).catch((e)=>{
                if (e.code===404)
                    this.setState({notFound:true});
                else if (tc>0)this.loadAll(tc-1);
            });
    }
    reload(){
        Api.message.getPageCount(this.state.topicId).then((c)=>this.setState({pageCount:c}));
        Api.message.getTopOld(this.state.topicId,this.state.page).then((list)=>this.setState({list}));
    }
    componentDidMount(){
        this.loadAll(5);
    }
    handleChange(){
        this.reload();
    }

    render(){
        if (this.state.notFound)
            return <NotFoundPage />
        if (this.state.topic===null)
            return <div>Loading...</div>
        return (<>
            <TopicView value={this.state.topic} user={this.props.user} />
            <MessageList list={this.state.list} user={this.props.user} onChange={this.handleChange}/>
            <MessageInput topicId={this.state.topicId} onSend={this.handleChange} user={this.props.user}/>
            <PageChooser current={this.state.page+1} count={this.state.pageCount} getUrl={(v)=>`/topic-${this.state.topicId}/page-${v}`}/>
        </>);
    }
} 
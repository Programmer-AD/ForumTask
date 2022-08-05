import React from 'react';
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom';
import Header from './Header/Header.jsx';
import MainPage from './Pages/MainPage/MainPage.jsx';
import ProfilePage from './Pages/ProfilePage/ProfilePage.jsx';
import TopicPage from './Pages/TopicPage/TopicPage.jsx';
import NotFoundPage from './Pages/NotFoundPage/NotFoundPage.jsx';
import css from "./style.module.css";
import Api from "../Api/ApiUnited.js"

export default class AppComponent extends React.Component {
    
    constructor(props) {
        super(props);

        this.state = { user: null };

        this.handleUserChanged = this.handleUserChanged.bind(this);
    }

    componentDidMount() {
        this.loadCurrentUser();
    }

    async loadCurrentUser() {
        try {
            const user = await Api.user.getCurrent();

            this.setState({ user });
        } catch {
            this.setState({ user: null });
        }
    }

    handleUserChanged() {
        this.loadCurrentUser();
    }

    render() {
        return (<>
            <Router>
                <Header user={this.state.user} onUserChanged={this.handleUserChanged} />
                <div className={css.page_container}>
                    <Switch>
                        <Route exact path="/" component={(props) => <MainPage user={this.state.user} {...props} />} />
                        <Route path="/page-:page(\d{1,9})" component={(props) => <MainPage user={this.state.user} {...props} />} />
                        <Route path="/topic-:topicId(\d{1,18})" component={(props) => <TopicPage user={this.state.user} {...props} />} />
                        <Route path="/topic-:topicId(\d{1,18})/page-:page(\d{1,9})" component={(props) => <TopicPage user={this.state.user} {...props} />} />
                        <Route path="/profile-:profileId(\d{1,18})" component={(props) => <ProfilePage user={this.state.user} {...props} />} />
                        <Route component={NotFoundPage} />
                    </Switch>
                </div>
            </Router>
        </>);
    }
}
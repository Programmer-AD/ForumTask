import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Header from './Header/Header.jsx';
import MainPage from './Pages/MainPage/MainPage.jsx';
import ProfilePage from './Pages/ProfilePage/ProfilePage.jsx';
import TopicPage from './Pages/TopicPage/TopicPage.jsx';
import NotFoundPage from './Pages/NotFoundPage/NotFoundPage.jsx';
import css from "./style.module.css";
import Api from "../Api/ApiUnited.js"

export default class AppComponent extends React.Component{
    constructor(props){
        super(props);

        this.state={user:null};
        this.getCurrentUser().then((user)=>this.setState({user}));

        this.handleUserChanged=this.handleUserChanged.bind(this);
    }
    async getCurrentUser(){
        let user=null;
        try{
            user=await Api.user.getCurrent();
        }catch(e){}
        return user;
    }
    handleUserChanged(){
        this.getCurrentUser().then((user)=>this.setState({user}));
    }
    render(){
        return (<>
            <Router>
                <Header user={this.state.user} onUserChanged={this.handleUserChanged} />
                <div className={css.page_container}>
                    <Switch>
                        <Route exact path="/" component={MainPage}/>
                        <Route path="/my" component={ProfilePage} />
                        <Route path="/profile/:profileId(\d{1,10}" component={ProfilePage} />
                        <Route path="/topic/:topicId(\d{1,19})" component={TopicPage}/>
                        <Route component={NotFoundPage} />
                    </Switch>
                </div>
            </Router>
        </>);
    }
}
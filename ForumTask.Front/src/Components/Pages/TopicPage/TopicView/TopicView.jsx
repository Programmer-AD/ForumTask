import React from "react";
import css from "./style.module.css";
import Api from "../../../../Api/ApiUnited.js";
import { Link } from "react-router-dom";

export default class TopicView extends React.Component{
    constructor(props){
        super(props);

        this.state={
            author:null
        };
    }

    componentDidMount(){
        Api.user.get(this.props.value.creatorId).then((a)=>this.setState({author:a}));
    }
    render(){
        return (<div className={css.container}>
            <h3 className={css.title}>{this.props.value.title}</h3>
            <div className={css.infos}>
                <span className={css.info}>Creator: {
                    this.state.author===null?"<Deleted user>"
                    :<Link to={`/profile-${this.state.author.id}`}>{this.state.author.userName}</Link>
                }</span>
                <span className={css.info}>Message count: {this.props.value.messageCount}</span>
                <span className={css.info}>{new Date(this.props.value.createTime+"Z").toLocaleString()}</span>
            </div>
        </div>);
    }
}
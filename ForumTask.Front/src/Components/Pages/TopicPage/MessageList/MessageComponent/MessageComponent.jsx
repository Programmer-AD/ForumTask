import React from "react";
import { Link } from "react-router-dom";
import Api from "../../../../../Api/ApiUnited.js";
import MarkSetter from "./MarkSetter/MarkSetter.jsx";
import css from "./style.module.css"

export default class MessageComponent extends React.Component{
    constructor(props){
        super(props);

        this.state={
            author:null
        };

        this.handleMarkChange=this.handleMarkChange.bind(this);
    }

    componentDidMount(){
        Api.user.get(this.props.value.authorId).then((a)=>this.setState({author:a}));
    }
    handleMarkChange(){
        this.props.onChange?.();
    }

    render(){
        return (<div className={css.container}>
            <div className={css.text}>{this.props.value.text}</div>
            <div className={css.infos}>
                <span className={css.info}>Author: {
                    this.state.author===null?"<Deleted user>"
                    :<Link to={`/profile-${this.state.author.id}`}>{this.state.author.userName}</Link>
                }</span>
                <span className={css.info}>
                    <MarkSetter user={this.props.user} value={this.props.value.mark} messageId={this.props.value.id} onChange={this.handleMarkChange}/>
                </span>
                <span className={css.info}>{new Date(this.props.value.writeTime).toLocaleString()}</span>
            </div>
        </div>);
    }
}
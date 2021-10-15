import React from "react";
import css from "./style.module.css"

export default class TopicComponent extends React.Component{
    render(){
        return (<div className={css.container}>
            <h3 className={css.title}>{this.props.value.title}</h3>
            <span className={css.messages}>Message count: {this.props.value.messageCount}</span>
            <span className={css.date}>{new Date(this.props.value.createTime).toLocaleString()}</span>
        </div>);
    }
}
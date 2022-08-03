import React from "react";
import { Link } from "react-router-dom";
import Button from "../../../../Common/Button/Button.jsx";
import css from "./style.module.css"

export default class TopicComponent extends React.Component{
    render(){
        return (<div className={css.container}>
            <h3 className={css.title}>{this.props.value.title}</h3>
            <div className={css.infos}>
                <Link to={`/topic-${this.props.value.id}`} style={{float:"left"}}><Button>Open</Button></Link>
                <span className={css.info}>Message count: {this.props.value.messageCount}</span>
                <span className={css.info}>{new Date(this.props.value.createTime+"Z").toLocaleString()}</span>
            </div>
        </div>);
    }
}
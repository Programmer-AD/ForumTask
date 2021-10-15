import React from "react";
import MessageComponent from "./MessageComponent/MessageComponent.jsx";
import css from "./style.module.css"

export default class MessageList extends React.Component{
    render(){
        return (<div className={css.container}>
            <h2 className={css.title}>List of messages</h2>
            {
                this.props.list.length===0?
                <div className={css.no_topics}>No Messages Found</div>
                :this.props.list.map((t)=><MessageComponent key={t.id} value={t}/>)
            }
        </div>);
    }
}
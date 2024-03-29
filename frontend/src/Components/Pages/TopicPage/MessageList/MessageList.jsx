import React from "react";
import MessageComponent from "./MessageComponent/MessageComponent.jsx";
import css from "./style.module.css"

export default class MessageList extends React.Component {
    render() {
        return (<div className={css.container}>
            <h2 className={css.title}>List of messages</h2>
            {
                this.props.list.length === 0 ?
                    <div className={css.no_topics}>No Messages Found</div>
                    : this.props.list.map(message => <MessageComponent key={message.id} value={message} user={this.props.user}
                        onChange={this.props.onChange} canModal={this.props.canModal} onModal={this.props.onModal} />)
            }
        </div>);
    }
}
import React from "react";
import Button from "../../../Common/Button/Button.jsx"
import css from "./style.module.css"

export default class SearchBar extends React.Component {
    render() {
        return (<div className={css.search_bar}>
            Find topic you need or create a new topic (to create a topic you have to login)<br />
            <Button onClick={() => this.props.onAdd?.()}>Create new topic</Button>
            <br />
            <input placeholder="Part of topic name" className={css.input} value={this.props.value} onChange={(event) => this.props.onChange?.(event.target.value)} />
            <Button onClick={() => this.props.onSearch?.()}>Search</Button>
        </div>);
    }
}
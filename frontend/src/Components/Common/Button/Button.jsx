import React from "react";
import css from "./style.module.css";

export default class Button extends React.Component {

    render() {
        return (<span onClick={this.props.onClick} className={css.button}>
            {this.props.children}
        </span>);
    }
}

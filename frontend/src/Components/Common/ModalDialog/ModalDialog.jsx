import React from "react";
import css from "./style.module.css";

export default class ModalDialog extends React.Component {
    render() {
        if (!this.props.visible) return null;
        return (<div className={css.modal_dialog}>
            <div className={css.header}>
                <img className={css.close_button} onClick={() => this.props.onClose?.()} src="/images/cross_button.png" />
            </div>
            <div>
                {this.props.children}
            </div>
        </div>);
    }
}

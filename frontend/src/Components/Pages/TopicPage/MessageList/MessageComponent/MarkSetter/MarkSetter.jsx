import React from "react";
import Api from "../../../../../../Api/ApiUnited.js";
import css from "./style.module.css"

export default class MarkSetter extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            ownMark: null
        };

        this.handleUp = this.handleUp.bind(this);
        this.handleDown = this.handleDown.bind(this);
    }

    componentDidMount() {
        if (this.props.user !== null) {
            Api.mark.getMark(this.props.messageId).then(ownMark => this.setState({ ownMark: ownMark }));
        }
    }

    handleUp() {
        if (this.state.ownMark !== null) {
            const newMark = this.state.ownMark === 1 ? 0 : 1;

            Api.mark.setMark(this.props.messageId, newMark).then(() => {
                this.setState({ ownMark: newMark });
                this.props.onChange?.(newMark);
            });
        }
    }
    handleDown() {
        if (this.state.ownMark !== null) {
            const newMark = this.state.ownMark === -1 ? 0 : -1;

            Api.mark.setMark(this.props.messageId, newMark).then(() => {
                this.setState({ ownMark: newMark });
                this.props.onChange?.(newMark);
            });
        }
    }

    render() {
        if (this.props.user === null) {
            return <span className={css.mark}>+{this.props.value.positiveCount}:-{this.props.value.negativeCount}</span>;
        }
        
        return (<span>
            <img src="/images/arrow_up.png" onClick={this.handleUp} className={css.arrow} set_up={(this.state.ownMark === 1).toString()} />
            <span className={css.mark}>+{this.props.value.positiveCount}:-{this.props.value.negativeCount}</span>
            <img src="/images/arrow_down.png" onClick={this.handleDown} className={css.arrow} set_down={(this.state.ownMark === -1).toString()} />
        </span>)
    }
}
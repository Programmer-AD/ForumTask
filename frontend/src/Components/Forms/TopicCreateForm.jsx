import React from "react";
import Api from "../../Api/ApiUnited.js";
import Button from "../Common/Button/Button.jsx";
import css from "./style.module.css";

export default class TopicCreateForm extends React.Component {
    
    constructor(props) {
        super(props);

        this.state = {
            title: "",
            message: "",
            authTry: null,
            validationError: null,
            success: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.tryCreate = this.tryCreate.bind(this);
    }

    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    tryCreate() {
        let validationError = "";

        if (this.state.title.length < 5){
            validationError += "Title is too short\r\n";
        }
        if (this.state.title.length > 60){
            validationError += "Title is too long\r\n";
        }
        if (this.state.message.length > 5000){
            validationError += "Message is too long\r\n";
        }

        if (validationError.length !== 0) {
            this.setState({ validationError: validationError });
        } else {
            let authTry = Api.topic.create(this.state.title, this.state.message);
            authTry.catch(() => {
                this.setState({ authTry: null, validationError: "Create data incorrect or some error occured" })
            });
            authTry.then((tid) => {
                this.setState({ authTry: null, success: true });
                this.props.onSuccess?.(tid);
            });
            this.setState({ validationError: null, authTry: authTry })
        }
    }

    render() {
        if (this.state.success)
            return (<div className={css.success}>Created successfully!</div>);
        return (<>
            <h2 className={css.title}>Create topic</h2>
            {this.state.validationError === null ? null :
                <pre className={css.validation}>
                    {this.state.validationError}
                </pre>}
            <fieldset className={css.container}>
                <label htmlFor="title">Title: </label>
                <input maxLength="60" name="title" style={{ width: "400px", fontSize: "large" }} placeholder="Title" value={this.state.title} onChange={this.handleChange} />
                <br />
                <label htmlFor="message">Text of message to attach (if empty than message won`t be created): </label>
                <br />
                <textarea maxLength="5000" className={css.textarea} cols="100" rows="10" placeholder="Message" name="message" value={this.state.message} onChange={this.handleChange} />
            </fieldset>
            {this.state.authTry === null ? <Button onClick={this.tryCreate}>Create</Button> : <span>Trying to create...</span>}
        </>);
    }
}
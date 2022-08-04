import React from "react";
import Api from "../../Api/ApiUnited.js";
import Button from "../Common/Button/Button.jsx";
import css from "./style.module.css";

export default class LoginForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            login: "",
            password: "",
            remember: false,
            authTry: null,
            validationError: null,
            success: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.tryLogin = this.tryLogin.bind(this);
    }

    handleChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    tryLogin() {
        let validationError = "";

        if (this.state.login.length === 0) {
            validationError += "Login is required\r\n";
        }
        if (this.state.password.length < 6) {
            validationError += "Too short password\r\n";
        }

        if (validationError.length !== 0) {
            this.setState({ validationError: validationError });
        } else {
            let authTry = Api.user.signIn(this.state.login, this.state.password, this.state.remember);
            authTry.catch(() => {
                this.setState({ authTry: null, validationError: "Login or password is incorrect or some error occurred" })
            });
            authTry.then(() => {
                this.setState({ authTry: null, success: true });
                this.props.onSuccess?.();
            });
            this.setState({ validationError: null, authTry: authTry, password: "" })
        }
    }

    render() {
        if (this.state.success)
            return (<div className={css.success}>Login successfully!</div>);
        return (<>
            <h2 className={css.title}>Login</h2>
            {this.state.validationError === null ? null :
                <pre className={css.validation}>
                    {this.state.validationError}
                </pre>}
            <fieldset className={css.container}>
                <label htmlFor="login">Login: </label>
                <input name="login" placeholder="login" value={this.state.login} onChange={this.handleChange} />
                <br />
                <label htmlFor="password">Password: </label>
                <input name="password" placeholder="password" type="password" value={this.state.password} onChange={this.handleChange} />
                <br />
                <input name="remember" type="checkbox" value={this.state.remember} onChange={this.handleChange} />
                <label htmlFor="remeber">Remember me </label>
                <br />
            </fieldset>
            {this.state.authTry === null ? <Button onClick={this.tryLogin}>Login</Button> : <span>Trying to login...</span>}
        </>);
    }
}
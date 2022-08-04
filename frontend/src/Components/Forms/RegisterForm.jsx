import React from "react";
import Api from "../../Api/ApiUnited.js";
import Button from "../Common/Button/Button.jsx";
import css from "./style.module.css";

export default class RegisterForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            login: "",
            email: "",
            password: "",
            confirmPassword: "",
            authTry: null,
            validationError: null,
            success: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.tryRegister = this.tryRegister.bind(this);
    }
    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    async tryRegister() {
        let validationError = "";

        const cantUseUserName = async () => !await Api.user.canUseUserName(this.state.login);
        const cantUseEmail = async () => !await Api.user.canUseEmail(this.state.email);

        if (this.state.login.length === 0) {
            validationError += "Login is required\r\n";
        } else if (this.state.email.length === 0) {
            validationError += "Email is required\r\n";
        } else if (await cantUseUserName()) {
            validationError += "This user name already in use\r\n";
        } else if (await cantUseEmail()) {
            validationError += "Email is already in use\r\n";
        }
        if (this.state.password.length < 6) {
            validationError += "Too short password\r\n";
        }
        if (this.state.password !== this.state.confirmPassword) {
            validationError += "Passwords mismatch\r\n";
        }

        if (validationError.length !== 0) {
            this.setState({ validationError: validationError });
        } else {
            let authTry = Api.user.register(this.state.login, this.state.email, this.state.password);

            authTry.catch(() => {
                this.setState({ authTry: null, validationError: "Register data is wrong or somer error occurred" })
            });
            authTry.then(() => {
                this.setState({ authTry: null, success: true });
                this.props.onSuccess?.();
            });

            this.setState({ validationError: null, authTry: authTry })
        }
    }

    render() {
        if (this.state.success)
            return (<div className={css.success}>Registred successfully!</div>);
        return (<>
            <h2 className={css.title}>Register</h2>
            {this.state.validationError === null ? null :
                <pre className={css.validation}>
                    {this.state.validationError}
                </pre>}
            <fieldset className={css.container}>
                <label htmlFor="login">Login: </label>
                <input name="login" placeholder="login" maxLength="255" value={this.state.login} onChange={this.handleChange} />
                <br />
                <label htmlFor="email">Email: </label>
                <input name="email" placeholder="email" maxLength="255" value={this.state.email} type="email" onChange={this.handleChange} />
                <br />
                <label htmlFor="password">Password: </label>
                <input name="password" placeholder="password" type="password" value={this.state.password} onChange={this.handleChange} />
                <br />
                <label htmlFor="confirmPassword">Confirm password: </label>
                <input name="confirmPassword" placeholder="Confirm password" type="password" value={this.state.confirmPassword} onChange={this.handleChange} />
            </fieldset>
            {this.state.authTry === null ? <Button onClick={this.tryRegister}>Register</Button> : <span>Trying to register...</span>}
        </>);
    }
}

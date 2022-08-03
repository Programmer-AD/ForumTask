import React from "react";
import Api from "../../Api/ApiUnited.js";
import Button from "../Common/Button/Button.jsx";
import css from "./style.module.css";

export default class RegisterForm extends React.Component{
    constructor(props){
        super(props);

        this.state={
            login:"",
            email:"",
            password:"",
            confirm:"",
            authTry:null,
            validErr:null,
            success:false
        };

        this.handleChange=this.handleChange.bind(this);
        this.tryRegister=this.tryRegister.bind(this);
    }
    handleChange(e){
        this.setState({[e.target.name]:e.target.value});
    }
    async tryRegister(){
        let ve="";
        if (this.state.login.length===0)
            ve+="Login is required\r\n";
        else if (!await Api.user.canUseUserName(this.state.login))
            ve+="This user name already in use\r\n";
        if (this.state.email.length===0)
            ve+="Email is required\r\n";
        else if (!await Api.user.canUseEmail(this.state.email))
            ve+="Email is already in use\r\n";
        if (this.state.password.length<6)
            ve+="Too short password\r\n";
        if (this.state.password!==this.state.confirm)
            ve+="Passwords mismatch\r\n";
        
        if (ve.length!==0){
            this.setState({validErr:ve});
        }else{
            let st=Api.user.register(this.state.login,this.state.email,this.state.password);
            st.catch(()=>{
                this.setState({authTry:null,validErr:"Register data is wrong or somer error occurred"})
            });
            st.then(()=>{
                this.setState({authTry:null,success:true});
                this.props.onSuccess?.();
            });
            this.setState({validErr:null,authTry:st})
        }
    }

    render(){
        if (this.state.success)
            return (<div className={css.success}>Registred successfully!</div>);
        return (<>
            <h2 className={css.title}>Register</h2>
            {this.state.validErr===null?null:
            <pre className={css.validation}>
                {this.state.validErr}
            </pre>}
            <fieldset className={css.container}>
                <label htmlFor="login">Login: </label>
                <input name="login" placeholder="login" maxLength="255" value={this.state.login} onChange={this.handleChange}/>
                <br/>
                <label htmlFor="email">Email: </label>
                <input name="email" placeholder="email" maxLength="255" value={this.state.email} type="email" onChange={this.handleChange}/>
                <br/>
                <label htmlFor="password">Password: </label>
                <input name="password" placeholder="password" type="password" value={this.state.password} onChange={this.handleChange}/>
                <br/>
                <label htmlFor="confirm">Confirm password: </label>
                <input name="confirm" placeholder="Confirm password" type="password" value={this.state.confirm} onChange={this.handleChange}/>
            </fieldset>
            {this.state.authTry===null?<Button onClick={this.tryRegister}>Register</Button>:<span>Trying to register...</span>}
        </>);
    }
}
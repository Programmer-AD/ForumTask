import React from "react";
import Api from "../../../Api/ApiUnited.js";
import Button from "../../Common/Button/Button.jsx";
import css from "./style.module.css";

export default class LoginForm extends React.Component{
    constructor(props){
        super(props);

        this.state={login:"",password:"",remeber:false,authTry:null,validErr:null};

        this.handleChange=this.handleChange.bind(this);
        this.tryLogin=this.tryLogin.bind(this);
    }
    handleChange(e){
        this.setState({[e.target.name]:e.target.value});
    }
    tryLogin(){
        let ve="";
        if (this.state.login.length===0)
            ve+="Login is required\r\n";
        if (this.state.password.length<6)
            ve+="Too short password\r\n";
        if (ve.length!==0){
            this.setState({validErr:ve});
        }else{
            let st=Api.user.signIn(this.state.login,this.state.password,this.state.remeber);
            st.catch(()=>{
                this.setState({authTry:null,validErr:"Login or password is incorrect"})
            });
            st.then(()=>{
                this.setState({authTry:null});
                this.props.onSuccess?.();
            });
            this.setState({validErr:null,authTry:st,password:""})
        }
    }

    render(){
        return (<>
            <h2 className={css.title}>Login</h2>
            {this.state.validErr===null?null:
            <pre className={css.validation}>
                {this.state.validErr}
            </pre>}
            <fieldset className={css.container}>
                <label htmlFor="login">Login: </label>
                <input name="login" value={this.state.login} onChange={this.handleChange}/>
                <br/>
                <label htmlFor="password">Password: </label>
                <input name="password" type="password" value={this.state.password} onChange={this.handleChange}/>
                <br/>
                <input name="remember" type="checkbox" value={this.state.remeber} onChange={this.handleChange}/>
                <label htmlFor="remeber">Remember me </label>
                <br/>
            </fieldset>
            {this.state.authTry===null?<Button onClick={this.tryLogin}>Login</Button>:<span>Trying to login...</span>}
        </>);
    }
}
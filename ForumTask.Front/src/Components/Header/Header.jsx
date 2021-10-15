import React from "react";
import { Link } from "react-router-dom";
import Api from "../../Api/ApiUnited.js";
import Button from "../Common/Button/Button.jsx";
import ModalDialog from "../Common/ModalDialog/ModalDialog.jsx";
import LoginForm from "../Forms/LoginForm.jsx";
import RegisterForm from "../Forms/RegisterForm.jsx";
import css from "./style.module.css";

export default class Header extends React.Component{
    constructor(props){
        super(props);

        this.state={showModal:false,modalPage:0};

        this.handleLogin=this.handleLogin.bind(this);
        this.handleRegister=this.handleRegister.bind(this);
        this.handleLogout=this.handleLogout.bind(this);
        this.handleClose=this.handleClose.bind(this);
    }

    handleLogin(){
        this.setState({showModal:true,modalPage:0});
    }
    handleRegister(){
        this.setState({showModal:true,modalPage:1});
    }
    handleLogout(){
        Api.user.signOut().then(()=>this.props.onUserChanged());
    }
    handleClose(){
        this.setState({showModal:false});
    }

    renderButtons(){
        if (!this.props.user){
            return (<>
                <Button onClick={this.handleLogin}>Login</Button>
                <Button onClick={this.handleRegister}>Register</Button>
            </>);
        }else{
            return (<>
                <Link to={`/profile-${this.props.user.id}`}>
                    <Button>My profile ({this.props.user.userName})</Button>
                </Link>
                <Button onClick={this.handleLogout}>Logout</Button>
            </>);
        }
    }
    render(){
        return (
        <div className={css.header}>
            <span className={css.title}>Our forum</span>
            <div className={css.button_container}>
                <Link to="/">
                    <Button>Home</Button>
                </Link>
                {this.renderButtons()}
            </div>
            <ModalDialog visible={this.state.showModal} onClose={this.handleClose}>
                {
                    this.state.modalPage===0?
                    <LoginForm onSuccess={()=>{
                        this.props.onUserChanged()
                    }}/>
                    :<RegisterForm/>
                }
            </ModalDialog>
        </div>);
    }
}
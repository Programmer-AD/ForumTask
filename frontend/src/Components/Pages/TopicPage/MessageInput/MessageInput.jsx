import React from "react";
import Api from "../../../../Api/ApiUnited.js";
import Button from "../../../Common/Button/Button.jsx";
import css from "./style.module.css"

export default class MessageInput extends React.Component{
    constructor(props){
        super(props);

        this.state={
            message:"",
            sending:false
        };

        this.handleChange=this.handleChange.bind(this);
        this.handleSend=this.handleSend.bind(this);
    }
    handleChange(e){
        this.setState({message:e.target.value});
    }
    handleSend(){
        if (this.state.message.length===0)
            alert("Message is required!");
        if (this.state.message.length>=5000)
            alert("Message is too long!");

        this.setState({sending:true});
        Api.message.send(this.props.topicId,this.state.message).then(()=>{
            this.setState({message:""});
            this.props?.onSend();
        }).finally(()=>this.setState({sending:false}));
    }
    render(){
        if (this.props.user===null)
            return null;
        if (this.props.user.isBanned)
            return (<div className={css.banned}>Your account is banned, so you can`t write messages</div>);
        return (<div className={css.message_input}>
            <textarea maxLength="5000" className={css.textarea} cols="100" rows="10" placeholder="Your message"
                value={this.state.message} onChange={this.handleChange}></textarea>
            <div>
                {this.state.sending?<span>Sending...</span>:<Button onClick={this.handleSend}>Send</Button>}
            </div>
        </div>)
    }
}
import React from "react";
import css from "./style.module.css";
import Api from "../../../../Api/ApiUnited.js";
import Button from "../../../Common/Button/Button.jsx";
import ModalDialog from "../../../Common/ModalDialog/ModalDialog.jsx";
import { Link } from "react-router-dom";

export default class TopicView extends React.Component{
    constructor(props){
        super(props);

        this.state={
            author:null,

            editModal:false,
            deleteModal:false,
            timeLimitModal:false,
            noTimeLimit:false,

            newTitle:this.props.value.title
        };
        this.handleOpenEdit=this.handleOpenEdit.bind(this);
        this.handleOpenDelete=this.handleOpenDelete.bind(this);
        this.handleClose=this.handleClose.bind(this);
        this.handleValueChange=this.handleValueChange.bind(this);
        this.handleSave=this.handleSave.bind(this);
        this.handleDelete=this.handleDelete.bind(this);
    }
    componentDidMount(){
        if (this.props.user!==null&&Api.getRoleId(this.props.user.roleName)!==0)
            this.setState({noTimeLimit:true});
        if (this.props.value.creatorId!==null)
            Api.user.get(this.props.value.creatorId).then((a)=>this.setState({author:a}));
    }

    checkTime(openModal=true){
        if (this.state.noTimeLimit)return true;
        if (new Date()-new Date(this.props.value.createTime+"Z")>=5*60*1000){
            if (openModal)this.openTimeModal();
            return false;
        }
        return true;
    }
    openTimeModal(){
        this.props.onModal?.(false);
        this.setState({editModal:false,deleteModal:false,timeLimitModal:true});
    }
    handleOpenEdit(){
        if (this.checkTime()&&this.props.canModal){
            this.props.onModal?.(false);
            this.setState({editModal:true,deleteModal:false,timeLimitModal:false});
        }
    }
    handleOpenDelete(){
        if (this.checkTime()&&this.props.canModal){
            this.props.onModal?.(false);
            this.setState({editModal:false,deleteModal:true,timeLimitModal:false});
        }
    }
    handleClose(){
        this.setState({editModal:false,deleteModal:false,timeLimitModal:false});
        this.props.onModal?.(true);
    }
    handleValueChange(e){
        this.setState({newTitle:e.target.value});
    }
    handleSave(){
        if (this.state.newTitle.length<5)
            alert("Topic is too short!");
        if (this.state.newTitle.length>60)
            alert("Topic is too long!");
        Api.topic.rename(this.props.value.id,this.state.newTitle).then(()=>{
            alert("Edited successfully!");
            this.handleClose();
            this.props.onChange?.();
        }).catch(()=>{
            alert("Edit error!");
        });
    }
    handleDelete(){
        Api.topic.delete(this.props.value.id).then(()=>{
            alert("Deleted successfully!");
            this.handleClose();
            this.props.onChange?.();
        }).catch(()=>{
            alert("Delete error!");
        });
    }

    renderButtons(){
        if (this.props.user===null)
            return null;
        let ur=Api.getRoleId(this.props.user.roleName),noEdit=false;
        if (ur===0&&!this.checkTime(false))
            return null;
        if (this.props.user.isBanned)
            return null;
        if (this.state.author===null)
            noEdit=true;
        if (this.state.author!==null&&this.props.user.id!=this.state.author.id){
            if (ur===0)
                return null;
            let udr=Api.getRoleId(this.state.author.roleName);
            if (udr>=ur)
                return null;
            noEdit=true;
        }
        return(<span className={css.button_container}>
            {
                noEdit?null:
                <Button onClick={this.handleOpenEdit}>Edit</Button>
            }
            <Button onClick={this.handleOpenDelete}>Delete</Button>
            <ModalDialog visible={this.state.editModal} onClose={this.handleClose}>
                <input className={css.edit_title} maxLength={60} onChange={this.handleValueChange} value={this.state.newTitle}/>
                <Button onClick={this.handleSave}>Save</Button>
            </ModalDialog>
            <ModalDialog visible={this.state.deleteModal} onClose={this.handleClose}>
                Do you really want to delete this topic?
                <Button onClick={this.handleDelete}>Yes</Button>
                <Button onClick={this.handleClose}>No</Button>
            </ModalDialog>
            <ModalDialog visible={this.state.timeLimitModal} onClose={this.handleClose}>
                Edit and delete time limit exceed
            </ModalDialog>
        </span>)
    }
    render(){
        return (<div className={css.container}>
            <h3 className={css.title}>{this.props.value.title}</h3>
            <div className={css.infos}>
                {this.renderButtons()}
                <span className={css.info}>Creator: {
                    this.state.author===null?"<Deleted user>"
                    :<Link to={`/profile-${this.state.author.id}`}>{this.state.author.userName}</Link>
                }</span>
                <span className={css.info}>Message count: {this.props.value.messageCount}</span>
                <span className={css.info}>{new Date(this.props.value.createTime+"Z").toLocaleString()}</span>
            </div>
        </div>);
    }
}
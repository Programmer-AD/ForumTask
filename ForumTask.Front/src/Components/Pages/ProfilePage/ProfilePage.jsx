import React from "react";
import Api from "../../../Api/ApiUnited";
import ModalDialog from "../../Common/ModalDialog/ModalDialog.jsx";
import css from "./style.module.css";

export default class ProfliePage extends React.Component{
    constructor(props){
        super(props);

        this.state={
            userData:null,
            notFound:false,

            modalText:"",
            showModal:false,
            onConfirm:null
        };

        
        this.handleChangeBan=this.handleChangeBan.bind(this);
        this.handleChangeRole=this.handleChangeRole.bind(this);
        this.handleDelete=this.handleDelete.bind(this);
        this.handleConfirm=this.handleConfirm.bind(this);
        this.handleCancel=this.handleCancel.bind(this);
    }

    load(tc=5){
        Api.user.get(this.props.match.params.profileId)
            .then((u)=>this.setState({userData:u}))
            .catch((e)=>{
                if (e.code===404)
                    this.setState({notFound:true});
                else if (tc>0)this.load(tc-1);
            })
    }
    handleChangeBan(){
        this.setState({
            showModal:true,
            modalText:`Do you really want to ${this.state.userData.isBanned?"Unban":"Ban"} user "${this.state.user.userName}"?`,
            onConfirm:()=>{
                Api.user.setBanned(this.state.userData.id,!this.state.userData.isBanned)
                .then(()=>this.load());
            }
        });
    }
    handleChangeRole(){
        let nrm=this.state.userData.roleName.toLower()==="user";
        this.setState({
            showModal:true,
            modalText:`Do you really want to set role "${nrm?"Moderator":"User"}" to user "${this.state.user.userName}"?`,
            onConfirm:()=>{
                Api.user.setRole(this.state.userData.id,"Moderator",nrm)
                .then(()=>this.load());
            }
        });
    }
    handleDelete(){
        this.setState({
            showModal:true,
            modalText:`Do you really want to delete your account?`,
            onConfirm:()=>{
                Api.user.delete(this.state.userData.id)
                .then(()=>this.load());
            }
        });
    }

    handleConfirm(){
        this.state.onConfirm?.();
        this.setState({showModal:false,modalText:"",onConfirm:null});
    }
    handleCancel(){
        this.setState({showModal:false,modalText:"",onConfirm:null});
    }

    componentDidMount(){
        this.load();
    }

    renderButtons(){
        let ur=this.props.user.roleName.toLower(),udr=this.state.userData.roleName;
        if (!this.props.user||(er==="user"||ur===udr))
            return null;
        let but=[];
        
        if (this.props.user.id!==this.state.userData.id){
            but.push(<Button onClick={this.handleChangeBan}>{this.state.userData.isBanned?"Unban":"Ban"}</Button>);
            if (ur==="admin") but.push(<Button onClick={this.handleChangeRole}>Make {udr==="user"?"moderator":"user"}</Button>);
        }else but.push(<Button onClick={this.handleDelete}>Delete</Button>);

        return (<div>
            {but}
        </div>);
    }

    render(){
        if (this.state.notFound)
            return <NotFoundPage />
        if (this.state.userData===null)
            return <div>Loading...</div>
        return (<>
            <h1 className={css.title}>User profile</h1>
            <div className={css.container}>
                <h2 className={css.userName}>{this.state.userData.userName}</h2>
                <span className={css.info}>Role: {this.state.userData.roleName}</span>
                <span className={css.info}>Registred at: {new Date(this.state.userData.registerDate+"Z").toLocaleString()}</span>
                {
                    this.state.userData.isBanned?
                    <span className={css.banned}>
                        Banned
                    </span>:<span className={css.not_banned}>
                        Not banned
                    </span>
                }
                {this.renderButtons()}
                <ModalDialog visible={this.state.showModal} onClose={this.handleCancel}>
                    <div className={css.modal_message}>
                        {this.state.modalText}
                    </div>
                    <div className={css.modal_buttons}>
                        <Button onClick={this.handleConfirm}>Yes</Button>
                        <Button onClick={this.handleCancel}>No</Button>
                    </div>
                </ModalDialog>
            </div>
        </>);
    }
}
import React from "react";
import SearchBar from "./SearchBar/SearchBar.jsx";
import TopicList from "./TopicList/TopicList.jsx";
import Api from "../../../Api/ApiUnited.js"
import css from "./style.module.css";
import ModalDialog from "../../Common/ModalDialog/ModalDialog.jsx";
import TopicCreateForm from "../../Forms/TopicCreateForm.jsx";

export default class MainPage extends React.Component{
    constructor(props){
        super(props);

        let page=this.props.match.params.page??1;
        if (page==0)
            this.props.history.push("/not-found");
        page--;
        this.state={list:[],search:"",page,showModal:false};

        this.handleChanged=this.handleChanged.bind(this);
        this.handleSearch=this.handleSearch.bind(this);
        this.handleClose=this.handleClose.bind(this);
        this.handleCreated=this.handleCreated.bind(this);
        this.handleAdd=this.handleAdd.bind(this);
    }
    load(){
        Api.topic.getTopNew(this.state.page,this.state.search)
            .then((list)=>this.setState({list}));
    }
    componentDidMount(){
        this.load();
    }

    handleChanged(search){
        this.setState({search});
    }
    handleSearch(){
        this.load();
    }
    handleAdd(){
        this.setState({showModal:true});
    }
    handleClose(){
        this.setState({showModal:false});
    }
    handleCreated(id){
        this.props.history.push(`/topic/${id}`);
    }

    render(){
        return (<>
            <h1 className={css.title}>Welcome to our forum!</h1>
            <SearchBar value={this.state.search} onChange={this.handleChanged} onSearch={this.handleSearch} onAdd={this.handleAdd}/>
            <TopicList list={this.state.list}/>
            <ModalDialog visible={this.state.showModal} onClose={this.handleClose}>
                {
                    this.props.user!==null&&!this.props.user.isBanned?
                    <TopicCreateForm onSuccess={this.handleCreated}/>
                    :<div>To create a new topic you must login to your account and your account mustn`t be blocked</div>
                }
            </ModalDialog>
        </>);
    }
} 
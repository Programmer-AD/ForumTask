import React from "react";
import SearchBar from "./SearchBar/SearchBar.jsx";
import TopicList from "./TopicList/TopicList.jsx";
import css from "./style.module.css";

export default class MainPage extends React.Component{
    constructor(props){
        super(props);

        this.state={list:[]};

        this.handleListChanged=this.handleListChanged.bind(this);
    }

    handleListChanged(list){
        this.setState({list:list});
    }

    render(){
        return (<>
            <h1 className={css.title}>Welcome to our forum!</h1>
            <SearchBar listChanged={this.handleListChanged} />
            <TopicList list={this.state.list}/>
        </>);
    }
} 
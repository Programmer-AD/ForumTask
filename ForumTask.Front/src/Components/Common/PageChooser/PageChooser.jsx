import React from "react";
import { NavLink } from "react-router-dom";
import css from "./style.module.css"

export default class PageChooser extends React.Component{
    constructor(props){
        super(props);

        let count=Math.min(props.count,9999);
        let pageSet=new Set();
        pageSet.add(props.current);
        for (let i=0;i<3&&i<count;i++)
            pageSet.add(i+1);
        for (let i=-2;i<=2;i++)
            if (props+i>0&&props+i<=count)
                pageSet.add(props.current+i);
        for (let i=0;i<3&&i<count;i++)
            pageSet.add(count-i);

        let pages=[];
        pageSet.forEach((v)=>pages.push(v));
        this.state={pages};
    }
    render(){
        return (<div className={css.container}>
            {
                this.state.pages.map((v)=><NavLink className={css.element} key={v} to={this.props.getUrl(v)}>{v}</NavLink>)
            }
        </div>);
    }
}
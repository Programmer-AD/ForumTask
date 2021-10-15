import React from "react";
import TopicComponent from "./TopicComponent/TopicComponent.jsx";
import css from "./style.module.css"

export default class TopicList extends React.Component{
    render(){
        return (<div className={css.container}>
            {
                this.props.list.length===0?
                <div className={css.no_topics}>No Topics Found</div>
                :this.props.list.map((t)=><TopicComponent key={t.id} value={t}/>)
            }
        </div>);
    }
}
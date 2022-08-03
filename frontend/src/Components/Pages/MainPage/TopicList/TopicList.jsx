import React from "react";
import TopicComponent from "./TopicComponent/TopicComponent.jsx";
import css from "./style.module.css"

export default class TopicList extends React.Component{
    render(){
        return (<div className={css.container}>
            <h2 className={css.title}>List of topics</h2>
            {
                (()=>{
                    if (!this.props.list)
                        return "Loading...";
                    if (this.props.list.length===0)
                        return (<div className={css.no_topics}>No Topics Found</div>);
                    return this.props.list.map((t)=><TopicComponent key={t.id} value={t}/>)
                })()
            }
        </div>);
    }
}
import React from "react";
import Api from "../../../../../../Api/ApiUnited.js";
import css from "./style.module.css"

export default class MarkSetter extends React.Component{
    constructor(props){
        super(props);

        this.state={
            ownMark:null
        };

        this.handleUp=this.handleUp.bind(this);
        this.handleDown=this.handleDown.bind(this);
    }

    componentDidMount(){
        if (this.props.user!==null)
            Api.mark.getMark(this.props.messageId).then((a)=>this.setState({ownMark:a}));
    }

    handleUp(){
        if (this.state.ownMark!==null){
            let nm=this.state.ownMark===1?0:1
            Api.mark.setMark(this.props.messageId,nm).then(()=>{
                this.setState({ownMark:nm});
                this.props.onChange?.(nm);
            });
        }
    }
    handleDown(){
        if (this.state.ownMark!==null){
            let nm=this.state.ownMark===-1?0:-1
            Api.mark.setMark(this.props.messageId,nm).then(()=>{
                this.setState({ownMark:nm});
                this.props.onChange?.(nm);
            });
        }
    }

    render(){
        if (this.props.user===null)
            return <span className={css.mark}>{this.props.value}</span>;
        return (<span>
            <img src="/images/arrow_up.png" onClick={this.handleUp} className={css.arrow} set_up={(this.state.ownMark===1).toString()}/>
            <span className={css.mark}>{this.props.value}</span>
            <img src="/images/arrow_down.png" onClick={this.handleDown} className={css.arrow} set_down={(this.state.ownMark===-1).toString()}/>
        </span>)
    }
}
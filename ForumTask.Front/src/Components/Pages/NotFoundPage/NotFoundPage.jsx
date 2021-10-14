import css from "./style.module.css"
import { Link } from "react-router-dom";
import React from "react";

export default class NotFoundPage extends React.Component{
    render(){
        return (
            <div className={css.page_container}>
                <h1 className={css.title}>Page Not Found</h1>
                <img src="/images/404.jpeg" />
                <Link to="/" className={css.link}>Return to home</Link>
            </div>
        );
    }
}
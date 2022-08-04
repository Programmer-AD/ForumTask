import React from "react";
import { NavLink } from "react-router-dom";
import css from "./style.module.css"

export default class PageChooser extends React.Component {
    constructor(props) {
        super(props);

        const count = props.count;
        const pageSet = new Set();

        //add 3 first pages
        for (let i = 0; i < 3 && i < count; i++) {
            pageSet.add(i + 1);
        }

        const isCorrectPageNumber = number => number > 0 && number <= count;

        //add 5 pages near current
        for (let i = -2; i <= 2; i++) {
            let pageNumber = i + props.current;

            if (isCorrectPageNumber(pageNumber)) {
                pageSet.add(pageNumber);
            }
        }

        //add 3 last pages
        for (let i = 0; i < 3 && i < count; i++) {
            pageSet.add(count - i);
        }

        this.state = { pages: [...pageSet] };
    }

    render() {
        return (<div className={css.container}>
            {
                this.state.pages.map(pageNumber => <NavLink className={css.element} key={pageNumber} to={this.props.getUrl(pageNumber)}>{pageNumber}</NavLink>)
            }
        </div>);
    }
}

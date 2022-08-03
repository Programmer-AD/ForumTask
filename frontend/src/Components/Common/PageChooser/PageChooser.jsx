import React from "react";
import { NavLink } from "react-router-dom";
import css from "./style.module.css"

export default class PageChooser extends React.Component {
    constructor(props) {
        super(props);

        let count = props.count;
        let pageSet = new Set();

        pageSet.add(props.current);

        for (let i = 0; i < 3 && i < count; i++) {
            pageSet.add(i + 1);
        }

        const isCorrectPageNumber = number => number > 0 && number <= count;

        for (let i = -2; i <= 2; i++) {
            let pageNumber = i + props.current;

            if (isCorrectPageNumber(pageNumber)) {
                pageSet.add(pageNumber);
            }
        }

        for (let i = 0; i < 3 && i < count; i++) {
            pageSet.add(count - i);
        }

        let pages = [...pageSet];

        this.state = { pages };
    }

    render() {
        return (<div className={css.container}>
            {
                this.state.pages.map(pageNumber => <NavLink className={css.element} key={pageNumber} to={this.props.getUrl(pageNumber)}>{pageNumber}</NavLink>)
            }
        </div>);
    }
}

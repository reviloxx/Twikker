import React from 'react';

export default class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loggedIn: false
        }
    }

    componentDidMount() {
        if (this.props.activeUserId > -1) {
            this.setState({ loggedIn: true });
        } else {
            this.setState({ loggedIn: false });
        }
    }

    render() {
        return (
            <ul className="nav navbar-nav" >
                <li><a onClick={this.props.onHomeClick}>Home</a></li>
                <li><a onClick={this.props.onLogoutClick}>Logout</a></li>
                <li><a onClick={this.props.onLoginClick}> Login</a></li>
            </ul>
            );


        //if (this.props.activeUserId > -1) {
        //    return (
        //        <ul className="nav navbar-nav" >
        //            <li><a onClick={this.props.onHomeClick}>Home</a></li>
        //            <li><a onClick={this.props.onLogoutClick}>Logout</a></li>
        //        </ul>);
        //} else {
        //    return (
        //        <ul className="nav navbar-nav" >
        //            <li><a onClick={this.props.onHomeClick}>Home</a></li>
        //            <li><a>Register</a></li>
        //            <li><a onClick={this.props.onLoginClick}> Login</a></li>
        //        </ul>);
        //}
    }
}
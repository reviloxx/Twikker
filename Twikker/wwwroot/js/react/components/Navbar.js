import React from 'react';

export default class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.user.userId,
            nickName: this.props.user.nickName
        }

        console.log(this.props);
    }

    componentWillReceiveProps() {
        this.state = {
            activeUserId: this.props.user.userId,
            nickName: this.props.user.nickName
        }

        console.log(this.props);
    }

    render() {
        if (this.state.activeUserId > -1) {
            return (
                <nav className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div class="navbar-header">
                        </div>
                        <div>
                            <ul className="nav navbar-nav">
                                <a className="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twikker</a>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Home</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Profile")}>{this.state.nickName}</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Logout")}>Logout</a></li>
                            </ul>
                        </div>
                    </div>
                </nav>
            );
        } else {
            return (
                <nav className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                        </div>
                        <div>
                            <ul className="nav navbar-nav" >
                                <a class="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twikker</a>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Home</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Register")}>Register</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Login")}>Login</a></li>
                            </ul>
                        </div>
                    </div>
                </nav>
            );
        }        
    }
}
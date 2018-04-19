import React from 'react';

export default class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.activeUserId
        }
    }

    componentWillReceiveProps() {
        this.state = {
            activeUserId: this.props.activeUserId
        }
    }

    render() {
        //console.log(this.state.activeUserId);
        if (this.state.activeUserId > -1) {
            return (
                <nav class="navbar navbar-inverse navbar-fixed-top">
                    <div class="container">
                        <div class="navbar-header">
                        </div>
                        <div id="navbar" class="navbar">
                            <ul className="nav navbar-nav" >
                                <a class="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twitler</a>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Home</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Profile")}>Profile</a></li>
                                <li><a href="#" onClick={this.props.onItemClicked.bind(this.props, "Logout")}>Logout</a></li>
                            </ul>
                        </div>
                    </div>
                </nav>
            );
        } else {
            return (
                <nav class="navbar navbar-inverse navbar-fixed-top">
                    <div class="container">
                        <div class="navbar-header">
                        </div>
                        <div id="navbar" class="navbar">
                            <ul className="nav navbar-nav" >
                                <a class="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twitler</a>
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
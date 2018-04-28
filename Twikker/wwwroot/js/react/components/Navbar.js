import React from 'react';

export default class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.user.userId,
            nickName: this.props.user.nickName
        };
    }

    componentWillReceiveProps() {
        this.state = {
            activeUserId: this.props.user.userId,
            nickName: this.props.user.nickName
        };
    }

    componentDidMount() {
        $(document).ready(function () {
            $('.nav a').on('click', function () {
                $('.navbar-toggle').click()
            });
        });
    }
            

    render() {
        if (this.state.activeUserId > -1) {
            return (
                <nav className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse-1" aria-expanded="false">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a className="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twikker</a>
                        </div>
                        <div id="navbar-collapse-1" className="collapse navbar-collapse">
                            <ul className="nav navbar-nav">                                
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
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse-1" aria-expanded="false">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a className="navbar-brand" href="#" onClick={this.props.onItemClicked.bind(this.props, "Home")}>Twikker</a>
                        </div>
                        <div id="navbar-collapse-1" className="collapse navbar-collapse">
                            <ul className="nav navbar-nav" >                                
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
Navbar.displayName = 'Navbar';
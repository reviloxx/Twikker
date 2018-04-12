import React from 'react';
import ReactDOM from 'react-dom';
import PostBox from './PostBox';
import LoginForm from './LoginForm';
import Navbar from './Navbar';

export default class Site extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: -1
        }
        //var activeUserId = -1;
    }

    

    componentWillMount() {
        this.getActiveUserId();        
    }

    getActiveUserId() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/getActiveUserId', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ activeUserId: data });
            console.log("data: " + data + " Active user: " + this.state.activeUserId);
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.renderHome();
        this.renderNavbar();         
    }    

    handleLogin() {
        this.getActiveUserId();        
        this.renderHome();
    }

    renderLoginForm(e) {
        ReactDOM.render(
            <LoginForm onLoggedIn={() => this.handleLogin()}></LoginForm >,
            document.getElementById('root')
        );
    }

    handleLogout() {
        var xhr = new XMLHttpRequest();
        xhr.open('post', '/logout', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ activeUserId: data });
            console.log("data: " + data + " Active user: " + this.state.activeUserId);
            ReactDOM.render(
                <div />,
                document.getElementById('root')
            );
            this.renderHome();
        }.bind(this);
        xhr.send();
    }

    renderHome() {
        ReactDOM.render(
            <PostBox url="/getPosts" submitUrl="posts/add" />,
            document.getElementById('root')
        );
    }

    renderNavbar() {
        ReactDOM.render(
            <Navbar activeUserId={this.activeUserId}
                onHomeClick={() => this.renderHome()}
                onLoginClick={() => this.renderLoginForm()}
                onLogoutClick={() => this.handleLogout()}></Navbar>,
            document.getElementById('navbar')
        );
    }   

    render() {
        return null;
    }
}

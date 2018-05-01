import React from 'react';
import ReactDOM from 'react-dom';
import PostBox from './PostBox';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';
import Navbar from './Navbar';
import UserProfile from './UserProfile';
import * as ajaxhandler from '../ajax-handler';

export default class Site extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: {
                userId: -1,
                nickname: '',
                firstname: '',
                lastname: '',
                email: ''
            },
            posts: [{
                comments: [],
                reactions: []
            }],

            postLoadCount: 5,
            morePostsAvailable: false,
            currentPage: null
        };
    }    

    componentWillMount() {
        this.getDataUpdateFromServer();
    }

    getDataUpdateFromServer() {
        this.getActiveUser();
        this.refreshPosts();
    }

    getActiveUser() {
        ajaxhandler.ajaxRequest(null, '/user/get', this.getActiveUserCallback.bind(this));
    }

    getActiveUserCallback(response) {
        this.setState({
            user: response
        });
        this.setState({
            user: response
        });
    }

    refreshPosts() {
        var data = new FormData();
        data.append('StartIndex', 0);
        data.append('Count', Math.max(this.state.posts.length, this.state.postLoadCount));

        ajaxhandler.ajaxRequest(data, '/posts/get', this.refreshPostsCallback.bind(this));
    }

    refreshPostsCallback(response) {
        this.setState({
            posts: response.posts,
            morePostsAvailable: response.morePostsAvailable,
            currentPage: null
        });
        this.setState({
            posts: response.posts,
            morePostsAvailable: response.morePostsAvailable
        });
        this.setState({
            posts: response.posts
        });
        this.setState({
            currentPage:
            <PostBox
                onPostsChanged={() => this.refreshPosts()}
                onRequestMorePosts={() => this.getMorePosts()}
                activeUserId={this.state.user.userId}
                posts={this.state.posts}
                morePostsAvailable={this.state.morePostsAvailable}
            />
        });
    }

    getMorePosts() {
        var data = new FormData();        
        data.append('StartIndex', this.state.posts.length);
        data.append('Count', this.state.postLoadCount);

        ajaxhandler.ajaxRequest(data, '/posts/get', this.getMorePostsCallback.bind(this));
    }

    getMorePostsCallback(response) {
        var newPosts = this.state.posts;

        for (var i = 0; i < response.posts.length; i++) {
            newPosts.push(response.posts[i]);
        }

        this.setState({
            posts: newPosts,
            morePostsAvailable: response.morePostsAvailable,
            currentPage: null
        });
        this.setState({
            morePostsAvailable: response.morePostsAvailable,
            posts: newPosts
        });
        this.setState({
            morePostsAvailable: response.morePostsAvailable,
            posts: newPosts
        });
        this.setState({
            currentPage:
            <PostBox
                onPostsChanged={() => this.refreshPosts()}
                onRequestMorePosts={() => this.getMorePosts()}
                activeUserId={this.state.user.userId}
                posts={this.state.posts}
                morePostsAvailable={this.state.morePostsAvailable}
            />
        });
    }

    sendLogout() {
        ajaxhandler.ajaxRequest(null, '/user/logout', this.logoutCallback.bind(this));
    }

    logoutCallback(response) {
        this.getDataUpdateFromServer();
    }
    
    onItemClickedCallback(identifier) {
        switch (identifier) {
            case "Home":
                this.refreshPosts();
                this.setState({
                    currentPage:
                    <PostBox
                        onPostsChanged={() => this.refreshPosts()}
                        onRequestMorePosts={() => this.getMorePosts()}
                        activeUserId={this.state.user.userId}
                        posts={this.state.posts}
                        morePostsAvailable={this.state.morePostsAvailable}
                    />
                });
                break;
            case "Register":
                this.setState({ currentPage: <RegistrationForm onRegistered={() => this.onItemClickedCallback('Login')} /> });
                break;
            case "Logout":
                this.sendLogout();
                this.setState({
                    currentPage:
                    <PostBox
                        onPostsChanged={() => this.refreshPosts()}
                        onRequestMorePosts={() => this.getMorePosts()}
                        activeUserId={this.state.user.userId}
                        posts={this.state.posts}
                        morePostsAvailable={this.state.morePostsAvailable}
                    />
                });
                break;
            case "Login":
                this.setState({ currentPage: <LoginForm onLoggedIn={() => this.getDataUpdateFromServer()} /> });
                break;
            case "Profile":
                this.setState({ currentPage: <UserProfile onUpdated={() => this.getActiveUser()} user={this.state.user} /> });
                break;
        }        
    }

    render() {
        return (
            <div>
                <Navbar user={this.state.user} onItemClicked={(identifier) => this.onItemClickedCallback(identifier)} />
                <div id="body-content" className="container body-content">
                    {this.state.currentPage}
                </div>
            </div>
        );
    }       
}
Site.displayName = 'Site';
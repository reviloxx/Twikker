import React from 'react';
import ReactDOM from 'react-dom';
import PostBox from './PostBox';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';
import Navbar from './Navbar';
import UserProfile from './UserProfile';

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
        this.getPosts(true);
    }

    getActiveUser() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', '/user/get', true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);            
            this.setState({
                user: data
            });
            this.setState({
                user: data
            });
        }.bind(this);
        xhr.send();
    }

    getPosts(refresh) {
        var data = new FormData();

        if (refresh) {
            data.append('StartIndex', 0);
            data.append('Count', Math.max(this.state.posts.length, this.state.postLoadCount));
        } else {
            data.append('StartIndex', this.state.posts.length);
            data.append('Count', this.state.postLoadCount);
        }
        
        var xhr = new XMLHttpRequest();
        xhr.open('post', "/posts/get", true);
        xhr.onload = function () {
            var response = JSON.parse(xhr.responseText);
            console.log(response);
            if (refresh) {
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
            } else {
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
            }
            this.setState({
                currentPage:
                <PostBox
                    onPostsChanged={() => this.getPosts(true)}
                    onRequestMorePosts={() => this.getPosts(false)}
                    activeUserId={this.state.user.userId}
                    posts={this.state.posts}
                    morePostsAvailable={this.state.morePostsAvailable}
                />
            });
        }.bind(this);
        xhr.send(data);
    }

    sendLogout() {
        var xhr = new XMLHttpRequest();
        xhr.open('post', '/user/logout', true);
        xhr.onload = function () {
            this.componentWillMount();
        }.bind(this);
        xhr.send();
    }
    
    onItemClickedCallback(identifier) {
        switch (identifier) {
            case "Home":
                this.getPosts(true);
                this.setState({
                    currentPage:
                    <PostBox
                        onPostsChanged={() => this.getPosts(true)}
                        onRequestMorePosts={() => this.getPosts(false)}
                        activeUserId={this.state.user.userId}
                        posts={this.state.posts}
                        morePostsAvailable={this.state.morePostsAvailable}
                    />
                });
                break;
            case "Register":
                this.setState({ currentPage: <RegistrationForm onRegistered={() => this.getPosts(true)} /> });
                break;
            case "Logout":
                this.sendLogout();
                this.setState({
                    currentPage:
                    <PostBox
                        onPostsChanged={() => this.getPosts(true)}
                        onRequestMorePosts={() => this.getPosts(false)}
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
                this.setState({ currentPage: <UserProfile onUpdated={() => this.getDataUpdateFromServer()} user={this.state.user} /> });
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
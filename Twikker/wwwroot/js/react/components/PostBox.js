import React from 'react';
import PostList from './PostList';
import PostForm from './PostForm';

export default class PostBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.activeUserId,
            posts: this.props.posts,
            morePostsAvailable: this.props.morePostsAvailable
        };
    }    

    componentWillReceiveProps() {
        this.state = {
            activeUserId: this.props.activeUserId,
            posts: this.props.posts,
            morePostsAvailable: this.props.morePostsAvailable
        };
    }

    render() {
        console.log(this.state.morePostsAvailable);
        if (this.state.activeUserId > -1) {
            if (this.state.morePostsAvailable) {
                return (
                    <div className="post-box" >
                        <h1>Latest Posts</h1>
                        <PostForm onAddedPost={this.props.onPostsChanged} />
                        <PostList onDeletedPost={this.props.onPostsChanged} onChangedComment={this.props.onPostsChanged} activeUserId={this.state.activeUserId} posts={this.state.posts} />
                        <button className="load-more-button btn btn-info" onClick={this.props.onRequestMorePosts}>Load more</button>
                    </div>
                );
            } else {
                return (
                    <div className="post-box" >
                        <h1>Latest Posts</h1>
                        <PostForm onAddedPost={this.props.onPostsChanged} />
                        <PostList onDeletedPost={this.props.onPostsChanged} onChangedComment={this.props.onPostsChanged} activeUserId={this.state.activeUserId} posts={this.state.posts} />
                    </div>
                );
            }            
        }
        if (this.state.morePostsAvailable) {
            return (
                <div className="post-box" >
                    <h1>Latest Posts</h1>
                    <PostList activeUserId={this.state.activeUserId} posts={this.state.posts} />
                    <button className="load-more-button btn btn-info" onClick={this.props.onRequestMorePosts}>Load more</button>
                </div>
            );
        } else {
            return (
                <div className="post-box" >
                    <h1>Latest Posts</h1>
                    <PostList activeUserId={this.state.activeUserId} posts={this.state.posts} />
                </div>
            );
        }        
    }
}
PostBox.displayName = 'PostBox';
import React from 'react';
import Post from './Post';
import CommentBox from './CommentBox';

export default class PostList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            posts: this.props.data.posts
        }
    }

    render() {
        console.log('PostList ' + this.props.data.posts);
        var postNodes = this.props.data.posts.map(function (post) {
            return (
                <div>
                    <Post postId={post.postId}
                        onDeletedPost={this.props.onDeletedPost}
                        creator={post.creatorNickname}
                        creatorId={post.creatorId}
                        creationDate={post.creationDate}
                        activeUserId={this.props.data.activeUserId}
                        key={post.postId} >
                        {post.content}
                    </Post>
                    <CommentBox postId={post.postId}
                        onChangedComment={this.props.onChangedComment}
                        activeUserId={this.props.data.activeUserId}
                        comments={post.comments}
                        key={post.postId} >
                    </CommentBox>
                </div>
            );
        }.bind(this));
        return (
            <div className="post-list">
                {postNodes}
            </div>
        );
    }
}
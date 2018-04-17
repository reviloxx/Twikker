import React from 'react';
import Post from './Post';
import CommentBox from './CommentBox';

export default class PostList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.activeUserId,
            posts: this.props.posts
        }
    }    

    render() {
        var postNodes = this.state.posts.map(function (post) {
            return (
                <div className="post-list">
                    <Post postId={post.postId}
                        onDeletedPost={this.props.onDeletedPost}
                        creator={post.creatorNickname}
                        creatorId={post.creatorId}
                        creationDate={post.creationDate}
                        activeUserId={this.props.activeUserId}
                        key={post.postId} >
                        {post.content}
                    </Post>                    
                    <CommentBox postId={post.postId}
                        onChangedComment={this.props.onChangedComment}
                        activeUserId={this.props.activeUserId}
                        comments={post.comments}
                        key={post.postId} >
                    </CommentBox>
                </div>
            );
        }.bind(this));
        return (
            <div>
                {postNodes}
            </div>
        );
    }
}
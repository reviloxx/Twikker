import React from 'react';
import CommentList from './CommentList';
import CommentForm from './CommentForm';

export default class CommentBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activeUserId: this.props.activeUserId,
            comments: this.props.comments
        };
    }

    render() {
        if (this.state.activeUserId > -1) {
            return (
                <div className="comment-box">
                    <CommentList onDeletedComment={() => this.props.onChangedComment()} comments={this.state.comments} activeUserId={this.state.activeUserId} />
                    <CommentForm postId={this.props.postId} onAddedComment={() => this.props.onChangedComment()} />
                </div>
            );
        }
        return (
            <div className="comment-box" >
                <CommentList comments={this.state.comments} activeUserId={this.state.activeUserId} />
            </div>
        );
    }
}
CommentBox.displayName = 'CommentBox';

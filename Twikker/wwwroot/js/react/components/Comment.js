import React from 'react';
import Reaction from './Reaction';

export default class Comment extends React.Component {
    //constructor(props) {
    //    super(props);
    //    this.state = { commentId: this }
    //}

    handleDeleteClick(e) {
        e.preventDefault();
        console.log("Delete Comment: " + this.props.commentId);
        var data = new FormData();
        data.append('commentId', this.props.commentId)

        var xhr = new XMLHttpRequest();
        xhr.open('post', "comments/delete", true);
        xhr.onload = function() {
            this.props.onDeletedComment();
        }.bind(this);
        xhr.send(data);
    }

    componentDidMount() {
        $(document).ready(function () {
            $('.comment').mouseenter(function () {
                $(this).children('.delete-button').fadeIn(100);
            });
            $('.comment').mouseleave(function () {
                $(this).children('.delete-button').fadeOut(100);
            });
        });
    };

    render() {
        if (this.props.activeUserId == this.props.creatorId) {
            return (
                <div className="comment">
                    <button className="delete-button btn-danger btn-sm" onClick={this.handleDeleteClick.bind(this)}>Delete</button>
                    <h3 className="creator">{this.props.creator}</h3>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h5 className="comment-text">{this.props.children}</h5>
                    <Reaction textId={this.props.textId} reactions={this.props.reactions} activeUserId={this.props.activeUserId} />
                </div>
            );
        } else {
            return (
                <div className="comment">
                    <h3 className="creator">{this.props.creator}</h3>
                    <h6 className="creation-date">{this.props.creationDate}</h6>
                    <h5 className="comment-text">{this.props.children}</h5>
                    <Reaction textId={this.props.textId} reactions={this.props.reactions} activeUserId={this.props.activeUserId} />
                </div>
            );
        }
    }
}
﻿import React from 'react';
import Reaction from './Reaction';
import * as ajaxhandler from '../ajax-handler';

export default class Comment extends React.Component {

    componentDidMount() {
        $(document).ready(function () {
            $('.comment').mouseenter(function () {
                $(this).children('.delete-button').fadeIn(100);
            });
            $('.comment').mouseleave(function () {
                $(this).children('.delete-button').fadeOut(100);
            });
        });
    }

    handleDeleteClick(e) {
        e.preventDefault();
        var data = new FormData();
        data.append('commentId', this.props.commentId);
        ajaxhandler.ajaxRequest(data, 'comments/delete', this.handleResponse.bind(this));
    }

    handleResponse(response) {
        this.props.onDeletedComment();
    }

    render() {
        if (this.props.activeUserId === this.props.creatorId) {
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
Comment.displayName = 'Comment';
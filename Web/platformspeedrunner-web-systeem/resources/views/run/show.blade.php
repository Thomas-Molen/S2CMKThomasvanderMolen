@extends('layouts.app')
@section('title', 'Ticket')
@section('content')
    <!-- Content Header (Page header) -->
    @if($run->active === 1 OR $authenticationHelper->isAdmin())
    <div class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-7">
                    <h1 class="m-0 text-dark">Run: <b>{{ $run->custom_name }}</b></h1>
                </div>
                <div class="col-4">
                    <h4 class="m-0 text-dark" style="float: right">Created on: {{ $run->created_at }} (UTC)</h4>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
{{--        Left Bar--}}
        <div class="row">
            <div class="col-7 left-column">
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Information:</h5>
                        <blockquote class="quote-info">
                            <p style="white-space: pre-line; word-wrap: break-word; margin-top:-3%">
                                {{ $run->information }}
                            </p>
                        </blockquote>
                    </div>
                </div>
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Comments:</h5>
                        @foreach($comments as $comment)
                            <div class="col-11">
                                <blockquote>
                                    <i class="fas fa-user"></i><b>{{ $comment->user->username}}</b>
                                    <small>{{ $comment->created_at }}</small>
                                    <p style="white-space: pre-line; word-wrap: break-word; margin-top:-3%">
                                        {{ $comment->content }}
                                    </p>
                                    @if($comment->user_id === auth()->id())
                                        <a class="btn btn-sm btn-secondary" href="{{ route('comment.edit',$comment->id) }}" style="margin-top: 1%"><i class="fa fa-fw fa-edit"></i> Edit</a>
                                    @endif
                                </blockquote>
                            </div>
                        @endforeach
                        <a class="btn btn-sm btn-success" href="{{ route('leaderboard_create_comment', $run->id) }}" style="margin-top: 1%"><i class="fas fa-fw fa-comments"></i> Add Comment</a>
                    </div>
                </div>
            </div>
{{--            Right Bar--}}
            <div class="col-4">
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">Details:</h5>
                        <div style="margin-left: 4%; margin-top: 2%">
                            Runner: {{ $run->user->username }}<br>
                            Time: {{ $readabilityHelper->FormatTime($run->duration) }} (m:s:ms)
                        </div>
                    </div>
                </div>
                <div class="row info-box shadow">
                    <div class="col-11">
                        <h5 class="card-header">External Links:</h5>
                        <div style="margin-left: 4%; margin-top: 2%">
                        @if($run->user_id === auth()->id() && count($links) !== 0)
                            <div style="margin-top: 1%; margin-bottom: -4%">
                                <a class="btn btn-sm btn-secondary" data-toggle="dropdown" aria-expanded="false" href="#"><i class="fa fa-fw fa-edit"></i>Edit Link</a>
                                <div class="dropdown-menu">
                                    @foreach($links as $link)
                                        <a class="dropdown-item" href="{{ route('link.edit',$link->id) }}">
                                            {{ $link->name }}
                                        </a>
                                    @endforeach
                                </div>
                            </div>
                        @endif
                        <br>
                        @foreach($links as $link)
                        <i class="fas fa-link"></i><a href="{{ $link->url }}" target="_blank" rel="noopener noreferrer">{{ $link->name }}</a><br>
                        @endforeach
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 1%">
                    <div class="col-9">
                        @if($run->user_id === auth()->id())
                            <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                <a class="btn btn-sm btn-success" href="{{ route('run_create_link', $run->id) }}"><i class="fas fa-link"></i> Add Link</a>
                                <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-edit"></i> Edit Run</a>

                                @csrf
                                @method('DELETE')

                                <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete Run</button>
                            </form>
                        @endif
                    </div>
                </div>
            </div>
        </div>
        <a class="btn btn-secondary" href="{{ $routingHelper->PreviousRoute() }}">Back</a><br><br>
    </div>
    @else
        <div class="alert alert-danger alert-dismissible">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
            <h5><i class="icon fas fa-ban"></i>Attention!</h5>
            This run has been archived and is no longer visible on the leaderboards or personal run lists!
        </div>
        <a class="btn btn-secondary" href="{{ route('leaderboard') }}" style="margin-left: 1%">Back</a>
    @endif
@endsection
<style>
    .left-column{
        margin-left: 1% !important;
    }
</style>

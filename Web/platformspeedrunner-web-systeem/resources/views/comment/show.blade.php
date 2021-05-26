@extends('layouts.app')

@section('template_title')
    {{ $comment->name ?? 'Show Comment' }}
@endsection

@section('content')
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Comment By: <strong>{{ $comment->user->username }}</strong></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="{{ route('comment.index') }}">Comments</a></li>
                        <li class="breadcrumb-item active">Show comment</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group">
                                <strong>ID:</strong>
                                {{ $comment->id }}
                            </div>
                            <div class="form-group">
                                <strong>Username:</strong>
                                {{ $comment->user->username }}
                            </div>
                            <div class="form-group">
                                <strong>Run:</strong>
                                {{ $comment->run->custom_name }}
                            </div>
                            <div class="form-group">
                                <strong>Content:</strong>
                                {{ $comment->content }}
                            </div>
                            <div class="form-group">
                                <strong>Created at:</strong>
                                {{ $comment->created_at }}
                            </div>
                            <div class="form-group">
                                <strong>Active:</strong>
                                {{ $comment->active }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ route('comment.index') }}">Back</a>
            <a class="btn btn-info" href="{{ route('comment.edit',$comment->id) }}">Edit</a>
        </div>
    </section>
@endsection

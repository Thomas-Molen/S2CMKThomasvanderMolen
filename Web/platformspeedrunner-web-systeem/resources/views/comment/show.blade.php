@extends('layouts.app')

@section('template_title')
    {{ $comment->name ?? 'Show Comment' }}
@endsection

@section('content')
    <section class="content container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="float-left">
                            <span class="card-title">Show Comment</span>
                        </div>
                        <div class="float-right">
                            <a class="btn btn-primary" href="{{ route('comment.index') }}"> Back</a>
                        </div>
                    </div>

                    <div class="card-body">

                        <div class="form-group">
                            <strong>User ID:</strong>
                            {{ $comment->user_id }}
                            <br>
                            <strong>Username:</strong>
                            {{ $comment->user->username }}
                        </div>
                        <div class="form-group">
                            <strong>Run ID:</strong>
                            {{ $comment->run_id}}
                            <br>
                            <strong>Run name:</strong>
                            {{ $comment->run->custom_name }}
                        </div>
                        <div class="form-group">
                            <strong>Content:</strong>
                            <br>
                            {{ $comment->content }}
                        </div>
                        <div class="form-group">
                            <strong>Active:</strong>
                            {{ $comment->active }}
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
@endsection

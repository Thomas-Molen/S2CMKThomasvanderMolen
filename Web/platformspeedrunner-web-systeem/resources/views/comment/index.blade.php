@extends('layouts.app')

@section('title')
    Comment
@endsection

@section('content')
    {{--Content Header (Page header)--}}
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Comments</h1>
                </div>
            </div>
        </div>
    </section>

    {{--Main Content--}}
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <a class="btn btn-primary" style="float: right" href="{{ route('comment.create') }}"><i class="fas fa-plus"></i> New comment</a>
                        </div>

                        @if ($message = Session::get('success'))
                            <div class="alert alert-success">
                                <p>{{ $message }}</p>
                            </div>
                        @endif

                        <div class="card-body">
                            <div class="table-responsive">
                                <table id="commentsTable" class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th>Date</th>
                                        <th>Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ($comments as $comment)
                                        @if ($comment->active === 1)
                                            <tr>
                                                <td>{{ $comment->id }}</td>
                                                <td>{{ (new App\Http\Controllers\UserController())->GetUsername($comment->user_id) }}</td>
                                                <td>{{ (new App\Http\Controllers\RunController())->GetName($comment->run_id) }}</td>
                                                <td>{{ (new App\Http\Controllers\CommentController())->ShowContent($comment->content) }}</td>
                                                <td>{{ $comment->created_at . " (UTC)"}}</td>
                                                <td>
                                                    <form action="{{ route('comment.destroy',$comment->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('comment.show',$comment->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('comment.edit',$comment->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                        @csrf
                                                        @method('DELETE')
                                                        <button type="submit" class="btn btn-danger btn-sm"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                    </form>
                                                </td>
                                            </tr>
                                        @endif
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th>Date</th>
                                        <th>Actions</th>
                                    </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
@endsection

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
                                        <th class="default-order">#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th>Date</th>
                                        <th>Deleted</th>
                                        <th class="no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ($comments as $comment)
                                            <tr>
                                                <td>{{ $comment->id }}</td>
                                                <td>{{ $comment->user->username }}</td>
                                                <td>{{ $comment->run->custom_name }}</td>
                                                <td>{{ $readabilityHelper->ShortenString($comment->content, 100) }}</td>
                                                <td>{{ $comment->created_at . " (UTC)"}}</td>
                                                <td>
                                                    @if ($comment->active)
                                                        no
                                                    @else
                                                        yes
                                                    @endif
                                                </td>
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
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>User</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th>Date</th>
                                        <th>Deleted</th>
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
@section('pagejs')
    @include('inc.datatablefiltering')
@endsection

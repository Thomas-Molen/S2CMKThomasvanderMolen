@extends('layouts.app')
@section('title', 'Tickets')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">Comments</h1>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th class="default-order">Date (y-m-d UTC)</th>
                                        <th class="no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( $comments as $comment)
                                        <tr>
                                            <td>{{ $comment->id }}</td>
                                            <td>{{ $comment->run->custom_name }}</td>
                                            <td>{{ $readabilityHelper->ShortenString($comment->content, 100) }}</td>
                                            <td>{{ $comment->created_at }}</td>
                                            <td>
                                                <a class="btn btn-sm btn-primary " href="{{ route('run.show',$comment->run_id) }}"><i class="fa fa-fw fa-eye"></i> Show Run</a>
                                                <a class="btn btn-sm btn-secondary" href="{{ route('comment.edit',$comment->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>
                                            </td>
                                        </tr>
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th>Content</th>
                                        <th>Date (y-m-d UTC)</th>
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

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
                                <table class="table table-bordered table-striped PersonalCommentsTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Run</th>
                                        <th>Content</th>
                                        <th>Date (y-d-m UTC)</th>
                                        <th class="actions">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( (new App\Http\Controllers\PersonalCommentsController)->GetComments($comments) as $comment)
                                        @if ($comment->active === 1)
                                            <tr>
                                                <td>{{ $comment->id }}</td>
                                                <td>{{ (new App\Http\Controllers\RunController())->GetName($comment->run_id) }}</td>
                                                <td>{{ (new App\Http\Controllers\CommentController())->ShowContent($comment->content) }}</td>
                                                <td>{{ $comment->created_at . " (UTC)"}}</td>
                                                <td>
                                                    <form action="{{ route('comment.destroy',$comment->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('run.show',$comment->run_id) }}"><i class="fa fa-fw fa-eye"></i> Show Run</a>
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('comment.edit',$comment->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                        @csrf
                                                        @method('DELETE')
                                                        <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this comment?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                    </form>
                                                </td>
                                            </tr>
                                        @endif
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th>Time (m:s:ms)</th>
                                        <th>Date (y-d-m UTC)</th>
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
    <script>
        window.onload = function () {
            $("table.PersonalCommentsTable").DataTable({
                language: DataTable.Language,
                pageLength: 25,
                columnDefs: [
                    { orderable: false, targets: ["actions"] }
                ],
                order: [[3, 'asc']]
            })
        }
        DataTable.init();
    </script>
@endsection

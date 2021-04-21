@extends('layouts.app')
@section('title', 'Tickets')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">Runs</h1>
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
                                <table class="table table-bordered table-striped PersonalRunsTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th>Time (m:s:ms)</th>
                                        <th>Date (y-d-m UTC)</th>
                                        <th class="actions">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( (new App\Http\Controllers\PersonalRunsController)->GetRuns($runs) as $run)
                                        @if ($run->active === 1)
                                            <tr>
                                                <td>{{ $run->id }}</td>
                                                @if ($run->custom_name === "#" . $run->id)
                                                <td><a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Add Name</a></td>
                                                    @else
                                                    <td>{{ $run->custom_name}}</td>
                                                @endif
                                                <td>{{(new App\Http\Controllers\LeaderboardController)->FormatTime($run->duration)}}</td>
                                                <td>{{ $run->created_at}}</td>
                                                <td><form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                        @csrf
                                                        @method('DELETE')
                                                        <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
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
            $("table.PersonalRunsTable").DataTable({
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

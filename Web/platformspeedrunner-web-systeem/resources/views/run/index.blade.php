@extends('layouts.app')

@section('title')
    Run
@endsection

@section('content')
    {{--Content Header (Page header)--}}
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Runs</h1>
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
                            <a class="btn btn-primary" style="float: right" href="{{ route('run.create') }}"><i class="fas fa-plus"></i> New run</a>
                        </div>

                        @if ($message = Session::get('success'))
                            <div class="alert alert-success">
                                <p>{{ $message }}</p>
                            </div>
                        @endif

                        <div class="card-body">
                            <div class="table-responsive">
                                <table id="runsTable" class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Player</th>
                                        <th>Time</th>
                                        <th>Date</th>
                                        <th>Deleted</th>
                                        <th>Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ($runs as $run)
                                            <tr>
                                                <td>{{ $run->custom_name }}</td>
                                                <td>{{ (new App\Http\Controllers\UserController)->GetUsername($run->user_id) }}</td>
                                                <td>{{ $run->duration }}</td>
                                                <td>{{ $run->created_at . " (UTC)"}}</td>
                                                <td>
                                                    @if ($run->active === 1)
                                                        no
                                                    @else
                                                        yes
                                                    @endif
                                                </td>
                                                <td>
                                                    <form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>

                                                        @csrf
                                                        @method('DELETE')
                                                        <button type="submit" class="btn btn-danger btn-sm" onclick="return confirm('Are you sure you want to delete this run?');"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                    </form>
                                                </td>
                                            </tr>
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>#</th>
                                        <th>Player</th>
                                        <th>Time</th>
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

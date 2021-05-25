@extends('layouts.app')
@section('title', 'Tickets')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">Leaderboard</h1>
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
                        @if ($message = Session::get('success'))
                            <div class="alert alert-success">
                                <p>{{ $message }}</p>
                            </div>
                        @endif
                        @if ($message = Session::get('error'))
                            <div class="alert alert-danger">
                                <p>{{ $message }}</p>
                            </div>
                        @endif
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th class="no-order">Rank</th>
                                        <th>Title</th>
                                        <th>Player</th>
                                        <th class="default-order">Time (m:s:ms)</th>
                                        <th>Date (y-m-d UTC)</th>
                                        <th class="no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( $runs as $run)
                                        <tr>
                                            @if ($run->position === 1)
                                                <td><i class="fas fa-trophy"></i>{{ $run->position }}</td>
                                            @elseif ($run->position === 2)
                                                <td><i class="fas fa-medal"></i>{{ $run->position }}</td>
                                            @elseif ($run->position === 3)
                                                <td><i class="fas fa-award"></i>{{ $run->position }}</td>
                                            @else
                                                <td>{{ $run->position }}</td>
                                            @endif
                                            <td>{{ $run->custom_name}}</td>
                                            <td>{{ $run->user->username }}</td>

                                            <td>{{ $readabilityHelper->FormatTime($run->duration) }}</td>
                                            <td>{{ $run->created_at}}</td>
                                            <td>
                                                    <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                    <a class="btn btn-sm btn-success" href="{{ route('leaderboard_create_comment', $run->id) }}"><i class="nav-icon fas fa-comments"></i> Comment</a>
                                                    @if( auth()->id() === $run->user->id)
                                                        <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>
                                                    @endif
                                            </td>
                                        </tr>
                                    @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>Rank</th>
                                        <th>Title</th>
                                        <th>Player</th>
                                        <th>Time (m:s:ms)</th>
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

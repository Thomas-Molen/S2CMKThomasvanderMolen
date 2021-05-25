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
                                <table class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th class="default-order">Time (m:s:ms)</th>
                                        <th>Date (y-d-m UTC)</th>
                                        <th class="actions no-order">Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( $runs as $run)
                                        @if ($run->active === 1)
                                            <tr>
                                                <td>{{ $run->id }}</td>
                                                @if ($run->custom_name === "#" . $run->id)
                                                <td><a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Add Name</a></td>
                                                    @else
                                                    <td>{{ $run->custom_name }}</td>
                                                @endif
                                                <td>{{ $readabilityHelper->FormatTime($run->duration) }}</td>
                                                <td>{{ $run->created_at }}</td>
                                                <td>
                                                    <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                    <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>
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

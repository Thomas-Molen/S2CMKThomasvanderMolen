@extends('layouts.app')

@section('title')
    Create comment
@endsection

@section('content')
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Create comment</h1>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">

                    @includeif('partials.errors')

                    <div class="card card-default">
                        <div class="card-body">
                            <form method="POST" action="{{ route('comment.store') }}"  role="form" enctype="multipart/form-data">
                                @csrf

                                @include('comment.form')

                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-secondary" href="{{ (new \App\Http\Helpers\RoutingHelper)->PreviousRoute() }}">Back</a>
        </div>
    </section>
@endsection

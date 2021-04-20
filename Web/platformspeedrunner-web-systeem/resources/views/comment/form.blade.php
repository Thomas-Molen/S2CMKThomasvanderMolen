<div class="box box-info padding-1">
    <div class="box-body">
        <div class="form-group">
            {{ Form::label('content') }}
            {{ Form::text('content', $comment->content, ['class' => 'form-control' . ($errors->has('content') ? ' is-invalid' : ''), 'placeholder' => 'Content']) }}
            @if ($comment->run_id === null)
            {{ Form::hidden('run_id', $run_id) }}
            @endif
            {!! $errors->first('content', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @if ((new App\Http\Helpers\AuthenticationHelper)->IsAdmin())
        <div class="form-group">
            {{ Form::label('user_id') }}
            {{ Form::text('user_id', $comment->user_id, ['class' => 'form-control' . ($errors->has('user_id') ? ' is-invalid' : ''), 'placeholder' => 'User Id']) }}
            {!! $errors->first('user_id', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('run_id') }}
            {{ Form::text('run_id', $comment->run_id, ['class' => 'form-control' . ($errors->has('run_id') ? ' is-invalid' : ''), 'placeholder' => 'Run Id']) }}
            {!! $errors->first('run_id', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        @endif
    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>

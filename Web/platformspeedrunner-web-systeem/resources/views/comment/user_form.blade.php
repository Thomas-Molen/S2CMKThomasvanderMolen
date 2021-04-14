<div class="box box-info padding-1">
    <div class="box-body">
        <div class="form-group">
            {{ Form::label('content') }}
            {{ Form::text('content', $comment->content, ['class' => 'form-control' . ($errors->has('content') ? ' is-invalid' : ''), 'placeholder' => 'Content']) }}
            {{ Form::hidden('run_id', $run_id) }}
            {!! $errors->first('content', '<div class="invalid-feedback">:message</p>') !!}
        </div>
    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
